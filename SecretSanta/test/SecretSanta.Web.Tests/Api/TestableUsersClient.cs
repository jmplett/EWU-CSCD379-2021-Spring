using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Tests.Api {


    public class TestableUsersClient : IUsersClient
    {
        public List<int> DeleteUserReturnValue {get; set;} = new();
        public int DeleteInvocationCount {get; set;}
        public Task DeleteAsync(int id)
        { 
            DeleteInvocationCount++;
            DeleteUserReturnValue.Remove(id);
            return Task.FromResult<ICollection<int>?>(DeleteUserReturnValue);
        }


        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<FullUser>? GetAllUsersReturnValue {get; set;} = new();
        public int GetAllAsyncInvocationCount {get; set;}
        public Task<ICollection<FullUser>?> GetAllAsync()
        {
            GetAllAsyncInvocationCount++;
            return Task.FromResult<ICollection<FullUser>?>(GetAllUsersReturnValue);
        }

        public Task<ICollection<FullUser>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public FullUser? GetAsyncFullUser { get; set; }
        public int GetAsyncInvocationCount { get; set; }
        public Task<FullUser?> GetAsync(int id)
        {
            GetAsyncInvocationCount++;

            if(GetAsyncFullUser is not null && id == GetAsyncFullUser.Id)
            {
                return Task.FromResult<FullUser?>(GetAsyncFullUser);
            }

            return Task.FromResult<FullUser?>(null);
        }

        public Task<FullUser> GetAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public int PostAsyncInvocationCount {get; set;}
        public List<FullUser> PostAsyncInvokedParameters {get; } = new();

        public Task<FullUser> PostAsync(FullUser fUser)
        {
            PostAsyncInvocationCount++;
            PostAsyncInvokedParameters.Add(fUser);
            return Task.FromResult(fUser);
        }

        public Task<FullUser> PostAsync(FullUser fUser, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }


        public int PutAsyncInvocationCount { get; set; }
        public List<UpdateUser> PutAsyncInvokedParameters { get; set; } = new();
        public Task PutAsync(int id, UpdateUser updateUser)
        {
            PutAsyncInvocationCount++;

            PutAsyncInvokedParameters[id].FirstName = updateUser.FirstName;
            PutAsyncInvokedParameters[id].LastName = updateUser.LastName;

            return Task.FromResult<ICollection<UpdateUser>?>(PutAsyncInvokedParameters);
        }

        public Task PutAsync(int id, UpdateUser updateUser, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
} 