using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NichiOnlineTest.Core.Data
{
    public partial class VinayTestDBContext
    {
        public override int SaveChanges()
        {
            SetTimestamp(GetUserId());
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SetTimestamp(GetUserId());
            return await base.SaveChangesAsync();
        }

        public void SetTimestamp(Guid loggedInUser)
        {
            var now = DateTime.UtcNow;

            if (ChangeTracker.HasChanges())
            {
                {
                    var changeSet = ChangeTracker.Entries<NisCategory>();

                    if (changeSet != null && changeSet.Any())
                    {
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Modified))
                        {
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Added))
                        {
                            entry.Entity.CreatedBy = loggedInUser;
                            entry.Entity.CreatedDate = now;
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                    }
                }
                {
                    var changeSet = ChangeTracker.Entries<NisQuestionAnswers>();

                    if (changeSet != null && changeSet.Any())
                    {
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Modified))
                        {
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Added))
                        {
                            entry.Entity.CreatedBy = loggedInUser;
                            entry.Entity.CreatedDate = now;
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                    }
                }
                {
                    var changeSet = ChangeTracker.Entries<NisQuestions>();

                    if (changeSet != null && changeSet.Any())
                    {
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Modified))
                        {
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Added))
                        {
                            entry.Entity.CreatedBy = loggedInUser;
                            entry.Entity.CreatedDate = now;
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                    }
                }
                {
                    var changeSet = ChangeTracker.Entries<NisSubcategory>();

                    if (changeSet != null && changeSet.Any())
                    {
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Modified))
                        {
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Added))
                        {
                            entry.Entity.CreatedBy = loggedInUser;
                            entry.Entity.CreatedDate = now;
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                    }
                }
                {
                    var changeSet = ChangeTracker.Entries<NisUserAnswers>();

                    if (changeSet != null && changeSet.Any())
                    {
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Modified))
                        {
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Added))
                        {
                            entry.Entity.CreatedBy = loggedInUser;
                            entry.Entity.CreatedDate = now;
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                    }
                }
                {
                    var changeSet = ChangeTracker.Entries<NisUserTestActivity>();

                    if (changeSet != null && changeSet.Any())
                    {
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Modified))
                        {
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                        foreach (var entry in changeSet.Where(c => c.State == EntityState.Added))
                        {
                            entry.Entity.CreatedBy = loggedInUser;
                            entry.Entity.CreatedDate = now;
                            entry.Entity.UpdatedBy = loggedInUser;
                            entry.Entity.UpdatedDate = now;
                        }
                    }
                }
            }
        }

        public Guid GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            Guid userID;

            if (httpContext == null)
                userID = Guid.Empty;
            else
                // If it returns null, even when the user was authenticated, you may try to get the value of a specific claim 
                userID = httpContext.User.FindFirst(ClaimTypes.PrimarySid) == null ? Guid.Empty : Guid.Parse(httpContext.User.FindFirst(ClaimTypes.PrimarySid).Value);

            return userID;
        }
    }
}
