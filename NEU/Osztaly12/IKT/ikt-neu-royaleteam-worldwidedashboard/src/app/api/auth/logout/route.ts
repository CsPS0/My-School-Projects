import { NextResponse } from 'next/server';
export async function POST() {
  try {
    const response = NextResponse.json({ success: true });
    response.cookies.set({
      name: 'auth_token',
      value: '',
      httpOnly: true,
      path: '/',
      expires: new Date(0), 
    });
    return response;
  } catch (error) {
    console.error('Logout error:', error);
    return NextResponse.json({ error: 'Internal server error' }, { status: 500 });
  }
}
