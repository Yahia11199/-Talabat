namespace Talabat.Application.Contracts;

public record ChangePasswordRequest(
		string CurrentPassword,
		string NewPassword
);

