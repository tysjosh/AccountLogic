using System;
using System.Runtime.Serialization;

namespace GunvorAssessment.Exceptions
{
    [Serializable]
    public class UnauthorizedAccountOperationException : GunvorAssessmentException
    {
        public UnauthorizedAccountOperationException() { }

        public UnauthorizedAccountOperationException(
            string message)
            : base(message) { }

        public UnauthorizedAccountOperationException(
            string message,
            Exception inner)
            : base(message, inner) { }

        public UnauthorizedAccountOperationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
