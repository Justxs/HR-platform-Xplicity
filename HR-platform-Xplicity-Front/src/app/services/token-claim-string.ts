interface TokenInterface{
    roleType: string
}

const object: TokenInterface = {
    roleType: "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
}

export const TokenConstants = object;