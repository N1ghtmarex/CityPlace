import NextAuth from 'next-auth'
  import KeycloakProvider from 'next-auth/providers/keycloak'
  
  const handler = NextAuth({
    providers: [
      KeycloakProvider({
        clientId: process.env.NEXT_PUBLIC_KEYCLOAK_CLIENT_ID!,
        clientSecret: process.env.NEXT_PUBLIC_KEYCLOAK_CLIENT_SECRET || '', // omit if public client
        issuer: `${process.env.NEXT_PUBLIC_KEYCLOAK_URL}/realms/${process.env.NEXT_PUBLIC_KEYCLOAK_REALM}`,
      }),
    ],
    secret: process.env.NEXTAUTH_SECRET,
    session: { strategy: 'jwt' },
    callbacks: {
      async jwt({ token, account, profile }) {
        // Persist key fields from Keycloak on first sign-in
        if (account) {
          token.provider = 'keycloak'
          token.accessToken = account.access_token
        }
        return token
      },
      async session({ session, token }) {
        // Make token fields available on the client
        session.accessToken = token.accessToken;
        session.user = session.user || {}
        ;(session.user as any).provider = token.provider
        return session
      },
    },
  })
  
  export { handler as GET, handler as POST }