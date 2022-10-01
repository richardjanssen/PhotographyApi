﻿namespace PhotographyApi.ViewModels;

public class AccountViewModel
{
    public AccountViewModel(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
}