import { NextResponse } from 'next/server';
import { verifyToken } from '@/lib/utils/auth';
import { cookies } from 'next/headers';
import { prisma } from '@/lib/utils/prisma';
import bcrypt from 'bcryptjs';
export async function GET() {
  try {
    const cookieStore = await cookies();
    const token = cookieStore.get('auth_token')?.value;
    if (!token) {
      return NextResponse.json({ user: null }, { status: 200 });
    }
    const payload = await verifyToken(token);
    if (!payload) {
      return NextResponse.json({ user: null }, { status: 200 });
    }
    const user = await prisma.user.findUnique({
      where: { id: payload.id as string },
      select: {
        id: true,
        username: true,
        avatarUrl: true,
        steamId: true,
        steamApiKey: true,
        exophaseUsername: true,
        youtubeHandle: true,
        favoriteArtist: true,
        lastFmUsername: true,
        trackerUrlR6: true,
        trackerUrlRL: true,
        trackerUrlLoL: true,
        trackerUrlBF6: true,
        trackerUrlFortnite: true,
      }
    });
    if (!user) {
      const response = NextResponse.json({ user: null }, { status: 200 });
      response.cookies.delete('auth_token');
      return response;
    }
    return NextResponse.json({ user });
  } catch (error) {
    console.error('Auth check error:', error);
    return NextResponse.json({ user: null }, { status: 200 });
  }
}
export async function PUT(req: Request) {
  try {
    const cookieStore = await cookies();
    const token = cookieStore.get('auth_token')?.value;
    if (!token) {
      return NextResponse.json({ error: 'Unauthorized' }, { status: 401 });
    }
    const payload = await verifyToken(token);
    if (!payload) {
      return NextResponse.json({ error: 'Unauthorized' }, { status: 401 });
    }
    const body = await req.json();
    const { 
      originalPassword, password, avatarUrl, steamId, steamApiKey, exophaseUsername, youtubeHandle, favoriteArtist, lastFmUsername,
      trackerUrlR6, trackerUrlRL, trackerUrlLoL, trackerUrlBF6, trackerUrlFortnite 
    } = body;
    const updateData: Record<string, string | null> = {};
    if (password) {
      if (!originalPassword) {
        return NextResponse.json({ error: 'Original password is required' }, { status: 400 });
      }
      const existingUser = await prisma.user.findUnique({
        where: { id: payload.id as string },
        select: { password: true }
      });
      if (!existingUser || !existingUser.password) {
        return NextResponse.json({ error: 'User not found' }, { status: 404 });
      }
      const isMatch = await bcrypt.compare(originalPassword, existingUser.password);
      if (!isMatch) {
        return NextResponse.json({ error: 'Incorrect original password' }, { status: 400 });
      }
      updateData.password = await bcrypt.hash(password, 10);
    }
    if (avatarUrl !== undefined) updateData.avatarUrl = avatarUrl === '' ? null : avatarUrl;
    if (steamId !== undefined) updateData.steamId = steamId === '' ? null : steamId;
    if (steamApiKey !== undefined) updateData.steamApiKey = steamApiKey === '' ? null : steamApiKey;
    if (exophaseUsername !== undefined) updateData.exophaseUsername = exophaseUsername === '' ? null : exophaseUsername;
    if (youtubeHandle !== undefined) updateData.youtubeHandle = youtubeHandle === '' ? null : youtubeHandle;
    if (favoriteArtist !== undefined) updateData.favoriteArtist = favoriteArtist === '' ? null : favoriteArtist;
    if (lastFmUsername !== undefined) updateData.lastFmUsername = lastFmUsername === '' ? null : lastFmUsername;
    if (trackerUrlR6 !== undefined) updateData.trackerUrlR6 = trackerUrlR6 === '' ? null : trackerUrlR6;
    if (trackerUrlRL !== undefined) updateData.trackerUrlRL = trackerUrlRL === '' ? null : trackerUrlRL;
    if (trackerUrlLoL !== undefined) updateData.trackerUrlLoL = trackerUrlLoL === '' ? null : trackerUrlLoL;
    if (trackerUrlBF6 !== undefined) updateData.trackerUrlBF6 = trackerUrlBF6 === '' ? null : trackerUrlBF6;
    if (trackerUrlFortnite !== undefined) updateData.trackerUrlFortnite = trackerUrlFortnite === '' ? null : trackerUrlFortnite;
    const updatedUser = await prisma.user.update({
      where: { id: payload.id as string },
      data: updateData,
      select: {
        id: true,
        username: true,
        avatarUrl: true,
        steamId: true,
        exophaseUsername: true,
        youtubeHandle: true,
        favoriteArtist: true,
        lastFmUsername: true,
        trackerUrlR6: true,
        trackerUrlRL: true,
        trackerUrlLoL: true,
        trackerUrlBF6: true,
        trackerUrlFortnite: true,
      }
    });
    return NextResponse.json({ success: true, user: updatedUser });
  } catch (error) {
    console.error('Profile update error:', error);
    return NextResponse.json({ error: 'Internal server error' }, { status: 500 });
  }
}
