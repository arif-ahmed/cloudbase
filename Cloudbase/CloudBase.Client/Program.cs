using System;
using CloudBase.ECommerceService;
using CloudBase.TenantService;

namespace CloudBase.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof(TenantsController);
            string s = t.Assembly.FullName.ToString();
            Console.WriteLine("The fully qualified assembly name " +
                              "containing the specified class is {0}.", s);
        }
    }
}
