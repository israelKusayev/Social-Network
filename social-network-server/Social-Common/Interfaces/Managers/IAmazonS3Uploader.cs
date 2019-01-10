namespace Social_Common.Interfaces.Managers
{
    public interface IAmazonS3Uploader
    {
        string UploadFile(string image, string guid);
    }
}