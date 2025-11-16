namespace Talabat.Domain.Consts;
public static class DefaultRoles
{
	public partial class Admin
	{
		public const string Name = nameof(Admin);
		public const string Id = "019a4071-fc29-767d-b6e9-9bfca1eadf3b";
		public const string ConcurrencyStamp = "019a4071-fc29-767d-b6e9-9bfff0b3536d";
	}

	public partial class Owner
	{
		public const string Name = nameof(Owner);
		public const string Id = "019a4071-fc29-767d-b6e9-9bfd907b282d";
		public const string ConcurrencyStamp = "019a4071-fc29-767d-b6e9-9c00c23e2c33";
	}

	public partial class Member
	{
		public const string Name = nameof(Member);
		public const string Id = "019a4071-fc29-767d-b6e9-9bfe272ea9a1";
		public const string ConcurrencyStamp = "019a4071-fc29-767d-b6e9-9c018b89cb34";
	}
}