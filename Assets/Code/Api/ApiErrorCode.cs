using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMB.Api
{
    [Serializable]
    internal enum ApiErrorCode
    {
        NoError = 0,
        UnknownError = 4,
        MalformedJson = 5,
        LoginIncorrect = 6,
        UsernameTaken = 7,
        UsernameLengthRequirement = 8,
        PasswordLengthRequirement = 9,
        SessionLengthRequirement = 10,
        GameSaveLengthRequirement = 11,
        InvalidSession = 12,
        NoSaveData = 13
    }
}
