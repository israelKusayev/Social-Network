﻿namespace Authorization_Common.Interfaces
{
    public interface IToken
    {
        string GenerateKey(string userId, string username,bool isAdmin = false);

        dynamic ValidaleToken(string token);
    }
}