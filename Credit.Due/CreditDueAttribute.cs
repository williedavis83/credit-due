namespace Credit.Due
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public abstract class CreditDueAttribute : Attribute
    {
        /// <summary>
        /// Total share of credit
        /// </summary>
        public string[]? Roles { get; set; }

        /// <summary>
        /// Any extra credit this contributer sould have
        /// </summary>
        public int ExtraShares { get; set; }

        /// <summary>
        /// The total credit due. Total number of roles (min: 1) + Extra Shares
        /// </summary>
        public int TotalShares
        {
            get
            {
                var roleCount = Roles?.Length ?? 0;
                if (roleCount == 0)
                    roleCount = 1;
                return roleCount + ExtraShares;
            }
        }

        public override string ToString()
        {
            string ListRoles()
            {
                if(Roles == null || Roles.Length == 0)
                    return string.Empty;
                return string.Join(", ", Roles);
            }
            var typeName = GetType().Name;
            if (typeName.EndsWith("Attribute"))
                typeName = typeName.Substring(0, typeName.Length - "Attribute".Length);
            return $"{typeName}: Roles: {ListRoles()} Extra: {ExtraShares} Total: {TotalShares}";
        }

    }
}