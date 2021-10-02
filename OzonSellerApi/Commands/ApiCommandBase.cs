using Newtonsoft.Json;
using NLog;
using OzonSellerApi.Interfaces;
using OzonSellerApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OzonSellerApi.Commands
{
	public class ApiCommandBase<TOut>
		where TOut : System.Collections.IList
	{
		[JsonIgnore]
		private readonly static ILogger logger = LogManager.GetCurrentClassLogger();

		[JsonIgnore]
		public IApiConnection Connection { get; set; }

		[JsonIgnore]
		protected static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None, NullValueHandling = NullValueHandling.Ignore };

		[JsonIgnore]
		private ApiCommandAttributeBase apiCommandAttribute;
		[JsonIgnore]
		private ApiCommandAttributeBase ApiCommandAttribute
		{
			get
			{
				if (apiCommandAttribute == null)
					apiCommandAttribute = GetType().GetCustomAttribute<ApiCommandAttributeBase>(true);

				return apiCommandAttribute;
			}
		}

		[JsonIgnore]
		public HttpMethod Method { get; set; }

		[JsonIgnore]
		public ApiMethodParamsBase MethodParameters { get; set; } = new ApiMethodParamsBase();

		[JsonIgnore]
		public string Url
		{
			get
			{
				if (ApiCommandAttribute == null)
					return string.Empty;

				return "/" + ApiCommandAttribute.SchemaVersion.ToString() + ApiCommandAttribute.Url;
			}
		}

		public ApiCommandBase()
		{
			Method = ApiCommandAttribute.Method;  //Url?
		}

		public ApiCommandBase(ApiConnection connection) : this()
		{
			Connection = connection;
		}

		[JsonIgnore]
		public string JsonResponse { get; protected set; }

		[JsonIgnore]
		public ApiSimpleResponseMessage<TOut> Response { get; protected set; }

		public virtual string Serialize()
		{
			var serializedData = JsonConvert.SerializeObject(MethodParameters, SerializerSettings);
			return serializedData;
		}

		protected virtual TOut Deserialize(string jsonData, HttpResponseMessage response)
		{
			var responseBase = JsonConvert.DeserializeObject<ApiResponseMessageBase>(jsonData, SerializerSettings);

			// response with errors
			if (responseBase.Code != null && responseBase.Code.Length > 0)
			{
				throw new ApiException(responseBase.Message + ", " + (responseBase.Details == null ? "" : string.Join(", ", responseBase.Details.Select(item => "'" + item + "'"))),
					response.Content,
					responseBase.Error);
			}

			Response = JsonConvert.DeserializeObject<ApiSimpleResponseMessage<TOut>>(jsonData, SerializerSettings);
			return Response.Result;
		}


		public virtual async Task<TOut> ExecuteAsync(ApiMethodParamsBase data = null)
		{
			if (Connection == null)
				throw new ApiException("Connection object is not defined");

			if (data != null)
			{
				MethodParameters = data;
			}

			logger.Info($"{this.GetType().Name} = {Url}, {data?.GetType().Name}");

			var request = Serialize();
			try
			{
				HttpResponseMessage response = await Connection.PostRequestAsync(request, Url, Method);
				
				var currentInfoMessage = $"{(int)response.StatusCode}. {response.ReasonPhrase}";
				logger.Info(currentInfoMessage);
				
				// if wrong api URL or server is broken we won't get json result
				if (response.Content.Headers.ContentType.MediaType == "text/plain")
					throw new ApiException("Unexpected response: " + currentInfoMessage + ", url: " + Url, response.Content);

				JsonResponse = await response.Content.ReadAsStringAsync();
				JsonResponse = JsonResponse.DecodeEncodedNonAsciiCharacters();
				
				var outResult = Deserialize(JsonResponse, response);

				switch ((int)response.StatusCode)
				{
					// documented response status codes
					case (int)HttpStatusCode.NotFound:
					case (int)HttpStatusCode.Forbidden:
					case (int)HttpStatusCode.Unauthorized:
					case (int)HttpStatusCode.Conflict:
					case (int)HttpStatusCode.InternalServerError:
					case (int)HttpStatusCode.OK:
						break;

					default:
						throw new ApiException("Unexpected http status code: " + currentInfoMessage, response.Content);
				}

				if (outResult.Count > 0 && outResult[0] is IDEntity)
				{
					logger.Info($"{this.GetType().Name} = {Url}, {data?.GetType().Name},OutID={(outResult[0] as IDEntity).ID}");
				}

				return outResult;
			}
			catch (Exception ex)
			{
				logger.Error(ex, $"Error requesting Url {Url}.\r\nRequest: {request}\r\nResponse: {JsonResponse}. {Environment.NewLine} {ex.Message}");
				throw;
			}
		}
	}
}
