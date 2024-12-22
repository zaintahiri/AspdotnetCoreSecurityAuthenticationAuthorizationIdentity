using Microsoft.AspNetCore.Authorization;

namespace AspdotnetCoreSecurityAuthenticationAuthorizationIdentity.Authorization
{

    // use this inside the builder.services, add requirement with policy
    //options.AddPolicy("HRManagerOnly", policy => policy.RequireClaim("Manager")
    //                                                  .RequireClaim("Department","HR")
    //
    //         .Requirements.Add(new HRManagerProbationRequirement(3));

    //ADD SERVICE AS WELL
    //builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();

    public class HRManagerProbationRequirement:IAuthorizationRequirement
    {
        public HRManagerProbationRequirement(int probationMonths)
        { 
            this.ProbationMonths= probationMonths;
        }
        public int ProbationMonths { get; set; }
    }

    public class HRManagerProbationRequirementHandler : AuthorizationHandler<HRManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirement requirement)
        {
           if(!context.User.HasClaim(x=>x.Type=="EmployeementDate"))
                return Task.CompletedTask;

            var value=context.User.FindFirst(x => x.Type == "EmployeementDate").Value;
            var empDate = DateTime.Parse(value);
            var period = DateTime.Now - empDate;
            if (period.Days > 30 * requirement.ProbationMonths)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
