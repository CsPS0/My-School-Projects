import { NextResponse } from 'next/server';
import { prisma } from '@/lib/utils/prisma';
import { verifyToken } from '@/lib/utils/auth';
import { cookies } from 'next/headers';
export async function GET() {
  try {
    const cookieStore = await cookies();
    const token = cookieStore.get('auth_token')?.value;
    if (!token) return NextResponse.json({ error: 'Unauthorized' }, { status: 401 });
    const payload = await verifyToken(token);
    if (!payload || !payload.id) return NextResponse.json({ error: 'Unauthorized' }, { status: 401 });
    const user = await prisma.user.findUnique({
      where: { id: payload.id as string },
      select: {
        steamId: true,
        youtubeHandle: true,
        favoriteArtist: true,
        dashboardStyle: true,
        hiddenWidgets: true,
      },
    });
    if (!user) return NextResponse.json({ error: 'User not found' }, { status: 404 });
    return NextResponse.json(user);
  } catch {
    return NextResponse.json({ error: 'Internal Server Error' }, { status: 500 });
  }
}
export async function PUT(req: Request) {
  try {
    const cookieStore = await cookies();
    const token = cookieStore.get('auth_token')?.value;
    if (!token) return NextResponse.json({ error: 'Unauthorized' }, { status: 401 });
    const payload = await verifyToken(token);
    if (!payload || !payload.id) return NextResponse.json({ error: 'Unauthorized' }, { status: 401 });
    const body = await req.json();
    const updatedUser = await prisma.user.update({
      where: { id: payload.id as string },
      data: {
        steamId: body.steamId,
        youtubeHandle: body.youtubeHandle,
        favoriteArtist: body.favoriteArtist,
        dashboardStyle: body.dashboardStyle,
        hiddenWidgets: body.hiddenWidgets,
      },
    });
    return NextResponse.json({ success: true, updatedUser });
  } catch {
    return NextResponse.json({ error: 'Internal Server Error' }, { status: 500 });
  }
}
