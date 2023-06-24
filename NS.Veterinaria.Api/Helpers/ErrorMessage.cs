using ErrorOr;

namespace NS.Veterinary.Api.Helpers
{
    public static class ErrorMessage
    {
        public static string GetErrorMessageIsEmptyOrNull(string propertie)
            => $"O campo {propertie} é obrigatório.";

        public static string GetErrorMessageMaxLenght(string propertie, int characters)
            => $"O campo {propertie} excdeu o limite de {characters} caracteres.";

        public static string GetErrorMessageMinLenght(string propertie, int characters)
            => $"O campo {propertie} necessita de no minimo {characters} caracteres.";

        public static Error GetErrorMessageCommit() => Error.Failure(description: "Não foi possivel salvar as informações");

        public static Error GetErrorMessageIsLockedOut()
            => Error.Failure(description: "Usuario temporariamete bloqueado devido a tentativas de login.");

        public static Error GetErrorMessageloginFailure()
            => Error.Failure(description: "Usuario ou senha incorreto.");
    }
}
