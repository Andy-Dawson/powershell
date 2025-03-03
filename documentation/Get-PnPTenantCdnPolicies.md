---
Module Name: PnP.PowerShell
title: Get-PnPTenantCdnPolicies
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantCdnPolicies.html
---
 
# Get-PnPTenantCdnPolicies

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns the CDN Policies for the specified CDN (Public | Private).

## SYNTAX

```powershell
Get-PnPTenantCdnPolicies -CdnType <SPOTenantCdnType> [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Retrieves the current CDN policies for the tenant for the specified CDN type.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantCdnPolicies -CdnType Public
```

Returns the policies for the specified CDN type

## PARAMETERS

### -CdnType
The type of cdn to retrieve the policies from

```yaml
Type: SPOTenantCdnType
Parameter Sets: (All)
Accepted values: Public, Private

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

