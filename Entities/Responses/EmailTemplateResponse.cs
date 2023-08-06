using Utilities.Enum;

namespace Entities.Responses
{
    public class EmailTemplateResponse
    {
        public long Id { get; set; }
        public EmailTypeEnums EmailType { get; set; }
        public string Template { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;

    }
}
