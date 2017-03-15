using System;
using System.Collections;
using System.Linq;

namespace NetLifeFighting.KnowTests.Common.Abstraction.Result
{
	///<summary>
	/// Результат операции. Используется как void для методов сервисов для исключения Exception'ов,
	/// то есть метод сервиса не должен бросать Exception, а возвращать <see cref="Result"/>
	/// c соответсвующими <see cref="Status"/> и <see cref="Message"/>.
	///</summary>
	public class Result
	{
		#region Ctor

		///<summary>
		/// Конструктор
		///</summary>
		public Result()
		{
			Status = ResultStatus.Unknown;
		}

		///<summary>
		/// Конструктор
		///</summary>
		///<param name="status">Статус</param>
		public Result(ResultStatus status)
		{
			Status = status;
		}

		///<summary>
		/// Конструктор
		///</summary>
		///<param name="status">Статус</param>
		///<param name="message">Сопроводительное сообщение</param>
		public Result(ResultStatus status, string message)
			: this(status)
		{
			Message = message;
		}

		///<summary>
		/// Конструктор
		///</summary>
		///<param name="status">Статус</param>
		///<param name="message">Сопроводительное сообщение</param>
		///<param name="data">Данные вовращаемые операцией</param>
		public Result(ResultStatus status, string message, object data)
			: this(status, message)
		{
			Data = data;
		}

		///<summary>
		/// Конструктор
		///</summary>
		///<param name="data">Данные вовращаемые операцией</param>
		public Result(object data)
		{
			Status = ResultStatus.Success;
			Data = data;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		public Result(string message, Exception exception)
		{
			Status = ResultStatus.Failure;
			Message = message;
			Exception = exception;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		public Result(string message, Exception exception, ResultStatus status)
		{
			Status = status;
			Message = message;
			Exception = exception;
		}

		#endregion Ctor

		#region Public Properties

		///<summary>
		/// Статус результата, см. <see cref="ResultStatus"/>
		///</summary>
		public ResultStatus Status { get; set; }

		///<summary>
		/// Возвращаемые операций данные
		///</summary>
		public object Data { get; set; }

		///<summary>
		/// Сопроводительное сообщение, null если Status == ResultStatus.Success
		///</summary>
		public string Message { get; set; }

		/// <summary>
		/// Исключение.
		/// </summary>
		public Exception Exception { get; set; }

		#endregion Public Properties

		///<summary>
		/// Метод возвращает true, если статус равен Success.
		///</summary>
		/// <remarks>Метод для компактного кода.</remarks>
		public bool IsSuccess
		{
			get { return Status == ResultStatus.Success; }
		}
	}

	///<summary>
	/// Параметризованный результат операции. Используется для возврата результата из методов сервисов
	/// для исключения Exception'ов, то есть метод сервиса не должен бросать Exception,
	/// а возвращать <see cref="Result"/> c соответсвующими
	/// <see cref="Result.Status"/> и <see cref="Result.Message"/>.
	///</summary>
	///<typeparam name="T">Тип возвращаемых данных</typeparam>
	public class Result<T> : Result
	{
		private T _data;

		#region Ctor

		///<summary>
		/// Конструктор
		///</summary>
		public Result()
		{
		}

		///<summary>
		/// Конструктор
		///</summary>
		///<param name="status">Статус</param>
		public Result(ResultStatus status) :
			base(status)
		{
			Data = default(T);
		}

		///<summary>
		/// Конструктор. <see cref="Result.Status"/> == ResultStatus.Success
		///</summary>
		///<param name="data">Данные результата</param>
		public Result(T data) :
			base(ResultStatus.Success)
		{
			Data = data;
		}

		///<summary>
		/// Конструктор.
		///</summary>
		///<param name="status">Статус</param>
		///<param name="data">Данные результата</param>
		public Result(ResultStatus status, T data) :
			base(status)
		{
			Data = data;
		}

		///<summary>
		/// Конструктор.
		///</summary>
		///<param name="status">Статус</param>
		///<param name="message">Сопроводительное сообщение</param>
		public Result(ResultStatus status, string message) :
			base(status, message)
		{
			Data = default(T);
		}

		public Result(ResultStatus status, string message, T data)
			: base(status, message)
		{
			Data = data;
		}

		#endregion Ctor

		#region Public Properties

		///<summary>
		/// Возвращаемые операций данные
		///</summary>
		public new T Data
		{
			get { return _data; }
			set
			{
				_data = value;
				base.Data = _data;
			}
		}

		#endregion Public Properties

		/// <summary>
		/// Метод для приведения результата с данными одного типа в результат с данными другого.
		/// </summary>
		/// <remarks>
		/// Метод для компактного кода, целесообразен только для случая
		/// когда <see cref="Result.IsSuccess"/> = false, чтобы не присать:
		/// <code>
		/// Result{TypeONE} or1 = ...
		/// if (!or1.IsSuccess)
		///     return new Result{TypeTWO}(or1.Status, or1.Message);
		/// </code>
		/// </remarks>
		///<typeparam name="TOut"></typeparam>
		///<returns></returns>
		public Result<TOut> Cast<TOut>() where TOut : class
		{
			return new Result<TOut>(Status, Message, Data as TOut);
		}

		/// <summary>
		/// Метод для представления результата к базовому классу <see cref="Result"/>
		/// </summary>
		/// <remarks>
		/// Необходим для передачи результата в COM. По причине отсутствия в нем Generic типов.
		/// </remarks>
		/// <returns>Базовый класс <see cref="Result"/></returns>
		public Result AsResult()
		{
			object comData = Data;
			var enumerableData = Data as IEnumerable;
			if (enumerableData != null)
			{
				comData = Enumerable.Cast<object>(enumerableData).ToArray();
			}
			return new Result(Status, Message, comData);
		}

		///<summary>
		/// Метод проверки данных результата на null.
		///</summary>
		/// <remarks>
		/// Пользователься аккуратно, так как если <see cref="Data"/> имеет примитивный
		/// тип (например, int), я не знаю как поведет себя метод.
		/// TODO: проверить
		/// </remarks>
		public bool IsDataNull
		{
			get
			{
				return Data == null;
			}
		}
	}
}
