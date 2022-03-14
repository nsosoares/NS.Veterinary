namespace NS.Veterinaria.Api.Helpers
{
    public static class ErrorMessage
    {
        public static string GetErrorMessageIsEmptyOrNull(string propertie)
            => $"O campo {propertie} é obrigatório.";

        public static string GetErrorMessageMaxLenght(string propertie, int characters)
            => $"O campo {propertie} excdeu o limite de {characters} caracteres.";

        public static string GetErrorMessageMinLenght(string propertie, int characters)
            => $"O campo {propertie} necessita de no minimo {characters} caracteres.";

        public static string GetErrorMessageCommit()
            => "Não foi possivel salvar as informações";

        public static string GetErrorMessageIsLockedOut()
            => "Usuario temporariamete bloqueado devido a tentativas de login.";

        public static string GetErrorMessageloginFailure()
            => "Usuario ou senha incorreto.";
    }
}
