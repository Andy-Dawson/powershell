﻿
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;
using System.Net;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADUser", DefaultParameterSetName = ParameterSet_LIST)]
    [RequiredMinimalApiPermissions("User.Read.All")]
    [Alias("Get-PnPAADUser")]
    public class GetAzureADUser : PnPGraphCmdlet
    {
        const string ParameterSet_BYID = "Return by specific ID";
        const string ParameterSet_LIST = "Return a list";
        const string ParameterSet_DELTA = "Return the delta";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYID)]
        public string Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public string Filter;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public string OrderBy;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public string[] Select;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DELTA)]
        public SwitchParameter Delta;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public string DeltaToken;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public int StartIndex = 0;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public int? EndIndex = 999;

        protected override void ExecuteCmdlet()
        {
            if(MyInvocation.InvocationName.ToLower().Equals("get-pnpaaduser"))
            {
                WriteWarning("Get-PnPAADUser is obsolete. Use Get-PnPAzureADUser instead which has the same parameters.");
            }

            if (PnPConnection.Current.ClientId == PnPConnection.PnPManagementShellClientId)
            {
                PnPConnection.Current.Scopes = new[] { "Directory.ReadWrite.All" };
            }
            if (ParameterSpecified(nameof(Identity)))
            {
                PnP.PowerShell.Commands.Model.AzureAD.User user;
                if (Guid.TryParse(Identity, out Guid identityGuid))
                {
                    user = PnP.PowerShell.Commands.Utilities.AzureAdUtility.GetUser(AccessToken, identityGuid);
                }
                else
                {
                    user = PnP.PowerShell.Commands.Utilities.AzureAdUtility.GetUser(AccessToken, WebUtility.UrlEncode(Identity), Select);
                }
                WriteObject(user);
            }
            else if (ParameterSpecified(nameof(Delta)))
            {
                var userDelta = PnP.PowerShell.Commands.Utilities.AzureAdUtility.ListUserDelta(AccessToken, DeltaToken, Filter, OrderBy, Select, StartIndex, EndIndex);
                WriteObject(userDelta);
            } 
            else
            {
                var users = PnP.PowerShell.Commands.Utilities.AzureAdUtility.ListUsers(AccessToken, Filter, OrderBy, Select, StartIndex, EndIndex);
                WriteObject(users, true);
            }
        }
    }
}