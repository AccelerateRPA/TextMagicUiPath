using System;
using System.Activities;
using System.ComponentModel;
using TextmagicRest;

namespace TextMagic.Workflow.Activities
{
    public class SendSMS : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("The phone number(s) to send this message to. The country code must be used e.g. 447123123123.")]
        public InArgument<string[]> ToPhoneNumbers { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("The body of the message to be sent.")]
        public InArgument<string> Message { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Your TextMagic Account Username. This can be found in your TextMagic Account settings: https://my.textmagic.com/online/account/settings.")]
        public InArgument<string> UserName { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Your TextMagic Account APIKey.  This can be found in your TextMagic Api settings: https://my.textmagic.com/online/api/settings.")]
        public InArgument<string> APIKey { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var phoneNumbers = ToPhoneNumbers.Get(context);
            var message = Message.Get(context);
            var userName = UserName.Get(context);
            var aPIKey = APIKey.Get(context);
            var client = new Client(userName, aPIKey);
            var link = client.SendMessage(message, phoneNumbers);
            if (link.Success)
            {

            }
            else
            {
                throw new Exception(String.Format("Message send failed: {0}", link.ClientException.Message));
            }

        }
    }
}
