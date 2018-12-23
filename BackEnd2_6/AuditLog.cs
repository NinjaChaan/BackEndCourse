using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd2_6
{
	public class AuditLogMessage {
		public enum MessageType {
			started,
			completed
		}
		public Guid Id { get; set; }
		public DateTime TimeStamp { get; set; }
		public string Action { get; set; }
		public string Ip { get; set; }
		public MessageType MType { get; set; }

		public AuditLogMessage() {
			Id = Guid.NewGuid();
			TimeStamp = DateTime.Now;
		}

		public AuditLogMessage(string action, string ip, MessageType type) {
			Id = Guid.NewGuid();
			TimeStamp = DateTime.Now;
			Action = action;
			MType = type;
			Ip = ip;
		}

		public override string ToString() {
			const string started = "started";
			const string completed = "completed";
			return $"An request from ip address {Ip} to {Action} {(MType == MessageType.started ? started : completed)} at {TimeStamp}";
		}
	}

	public class AuditLogFilterAttribute : ActionFilterAttribute, IActionFilter {

		private string _action;

		public AuditLogFilterAttribute(string action) {
			_action = action;
		}

		public override void OnActionExecuting(ActionExecutingContext context) {
			IRepository repository = context.HttpContext.RequestServices.GetService<IRepository>();

			AuditLogMessage message = new AuditLogMessage(_action, context.HttpContext.Connection.RemoteIpAddress.ToString(), AuditLogMessage.MessageType.started);

			repository.RecordAuditMessage(message);
		}

		public override void OnActionExecuted(ActionExecutedContext context) {
			IRepository repository = context.HttpContext.RequestServices.GetService<IRepository>();

			AuditLogMessage message = new AuditLogMessage(_action, context.HttpContext.Connection.RemoteIpAddress.ToString(), AuditLogMessage.MessageType.completed);

			repository.RecordAuditMessage(message);
		}
	}
}
