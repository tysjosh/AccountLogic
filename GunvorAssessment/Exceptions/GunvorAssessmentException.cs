using System;
using System.Runtime.Serialization;

namespace GunvorAssessment.Exceptions
{
    [Serializable]
    public class GunvorAssessmentException : Exception
    {
        public GunvorAssessmentException() { }

        public GunvorAssessmentException(
            string message)
            : base(message) { }

        public GunvorAssessmentException(
            string message,
            Exception inner)
            : base(message, inner) { }

        public GunvorAssessmentException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
