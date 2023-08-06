﻿using System.Net;
using Utilities.Constants;

namespace Entities.SystemModels
{
    public class EmailContent
    {
        public string Message { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;

        public string Attachments { get; set; } = string.Empty;

        private List<string> _AttachmentUrls { get; set; }
        public List<string> AttachmentUrls
        {
            get
            {
                if ((_AttachmentUrls == null))
                {
                    _AttachmentUrls = ((string.IsNullOrEmpty(Attachments)) ? (new List<string>()) : (WebUtility.HtmlDecode(Attachments).Split(Constants.Comma).ToList()));

                    _AttachmentUrls.ForEach(r => r.Trim());
                    return _AttachmentUrls;
                }

                return _AttachmentUrls;
            }
        }
    }
}
