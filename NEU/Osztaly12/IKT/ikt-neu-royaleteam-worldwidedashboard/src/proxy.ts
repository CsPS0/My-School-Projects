import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';
import { verifyToken } from '@/lib/utils/auth';
const protectedRoutes = [
  '/gaming',
  '/entertainment',
  '/music',
  '/crypto',
  '/exchange',
  '/politics',
  '/settings'
];
const authRoutes = [
  '/login',
  '/signup'
];
export async function proxy(request: NextRequest) {
  const { pathname } = request.nextUrl;
  const isProtectedRoute = protectedRoutes.some(route => pathname.startsWith(route));
  const isAuthRoute = authRoutes.some(route => pathname.startsWith(route));
  const token = request.cookies.get('auth_token')?.value;
  let isValidUser = false;
  if (token) {
    const payload = await verifyToken(token);
    if (payload) {
      isValidUser = true;
    }
  }
  if (isProtectedRoute && !isValidUser) {
    return NextResponse.redirect(new URL('/login', request.url));
  }
  if (isAuthRoute && isValidUser) {
    return NextResponse.redirect(new URL('/overview', request.url));
  }
  return NextResponse.next();
}
export const config = {
  matcher: ['/((?!api|_next/static|_next/image|favicon.ico|sitemap.xml|robots.txt).*)',
  ],
};
