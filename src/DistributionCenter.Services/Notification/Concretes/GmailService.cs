namespace DistributionCenter.Services.Notification.Concretes;

using Bases;

public class GmailService(string username, string password)
    : BaseEmailService("smtp.gmail.com", 587, username, password) { }
