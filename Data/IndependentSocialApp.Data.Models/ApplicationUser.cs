﻿// ReSharper disable VirtualMemberCallInConstructor
namespace IndependentSocialApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using IndependentSocialApp.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.FriendShipRequester = new HashSet<Friendship>();
            this.FriendshipAdressee = new HashSet<Friendship>();
            this.Posts = new HashSet<Post>();
        }

        public Profile Profile { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Friendship> FriendShipRequester { get; set; }

        public virtual ICollection<Friendship> FriendshipAdressee { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
