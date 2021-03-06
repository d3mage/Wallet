﻿namespace BLL
{
    public class GetInputService : IGetInputService
    {
        private IReadUserInputService readInputService;
        private IVerifyInputService verifyInputService;

        public GetInputService(IReadUserInputService readInput, IVerifyInputService verifyInput)
        {
            readInputService = readInput;
            verifyInputService = verifyInput;
        }

        public string GetVerifiedInput(string pattern)
        {
            string input = readInputService.ReadInput();
            bool isInputProper = verifyInputService.isInputCorrect(input, pattern);
            for (int i = 0; i < 2; ++i)
            {
                if (isInputProper != true)
                {
                    input = readInputService.ReadInput();
                    isInputProper = verifyInputService.isInputCorrect(input, pattern);
                }
            }
            return isInputProper ? input.ToLower() : throw new TooManyFalseAttemptsException();
        }


    }
}