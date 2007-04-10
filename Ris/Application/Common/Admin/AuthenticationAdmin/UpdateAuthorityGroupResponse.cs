using System;
using System.Runtime.Serialization;

using ClearCanvas.Enterprise.Common;

namespace ClearCanvas.Ris.Application.Common.Admin.AuthenticationAdmin
{
    [DataContract]
    public class UpdateAuthorityGroupResponse : DataContractBase
    {
        public UpdateAuthorityGroupResponse(AuthorityGroupSummary authorityGroupSummary)
        {
            AuthorityGroupSummary = authorityGroupSummary;
        }

        [DataMember]
        public AuthorityGroupSummary AuthorityGroupSummary;
    }
}
