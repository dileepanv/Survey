namespace Survey_project.GlobalVariables
{

    public class Constants
    {
        private static string capturePayload;

        public static string CapturePayload { get => capturePayload; set => capturePayload = value; }


        #region Smtp Configuration
        public const string SmtpServer = "EmailConfiguration:SmtpServer";
        public const string SmtpPort = "EmailConfiguration:Port";
        public const string SmtpUserName = "EmailConfiguration:Username";
        public const string SmtpPassword = "EmailConfiguration:Password";
        public const string SmtpFrom = "EmailConfiguration:From";
        #endregion

        #region Success Messages
        public const string MailSentSuccesfully = "MailSentSuccessfully";
        public const string SuccessResponse = "Success";
        public const string CheckEmail = "Invalid Email";
        public const string Invalid = "Invalid";
        public const string Login = "login using a token";
        public const string Registration = "Registration Successful, Please Verify your Email";

        #endregion

        #region File Extension
        public static string GetPayload;
        public const string PDF = "pdf";
        public const string JPG = "jpg";
        public const string PNG = "png";
        public const string MP4 = "mp4";
        public const string PngExt = ".png";
        public const string PngSubString = "IVBOR";
        public const string JpgExt = ".jpg";
        public const string JpgSubString = "/9J/4";
        public const string mp4Ext = ".mp4";
        public const string mp4SubString = "AAAAF";
        public const string pdfExt = ".pdf";
        public const string pdfSubString = "JVBER";
        public const string icoExt = ".ico";
        public const string icoSubString = "AAABA";
        public const string rarExt = ".rar";
        public const string rarSubString = "UMFYI";
        public const string rtfExt = ".rtf";
        public const string rtfSubString = "E1XYD";
        public const string txtExt = ".txt";
        public const string txtSubString = "U1PKC";
        public const string srtExt = ".srt";
        public const string srtSubStringa = "MQOWM";
        public const string srtSubStringb = "77U/M";
        #endregion

        #region ImagePath
        public const string ProfileImage = "pumpkart/profilephoto";
        public const string GstImage = "pumpkart/gst";
        public const string PanImage = "pumpkart/pan";
        public const string ProductImage = "pumpkart/productimage";
        #endregion
    };
}

