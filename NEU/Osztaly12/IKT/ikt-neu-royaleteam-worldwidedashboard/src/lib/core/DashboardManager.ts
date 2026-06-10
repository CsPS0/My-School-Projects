import { IDataSource } from './DataSource';
import { prisma } from '../utils/prisma';
const CACHE_TTL_MS = 1000 * 60 * 5; 
export class DashboardManager {
  private providers: Map<string, IDataSource<unknown>> = new Map();
  registerProvider(provider: IDataSource<unknown>): void {
    if (this.providers.has(provider.id)) {
      console.warn(`Provider with ID ${provider.id} is already registered. Overwriting.`);
    }
    this.providers.set(provider.id, provider);
  }
  async fetchAllData(): Promise<Record<string, unknown>> {
    const results: Record<string, unknown> = {};
    const system_status: Record<string, { id: string; name: string; isMock: boolean }> = {};
    const fetchPromises = Array.from(this.providers.values()).map(async (provider) => {
      try {
        const data = await this.fetchWithCache(provider);
        if (data !== null) {
          results[provider.id] = data;
        }
        system_status[provider.id] = { id: provider.id, name: provider.name, isMock: provider.isMock };
      } catch (error) {
        console.error(`Error in provider ${provider.name}:`, error);
        system_status[provider.id] = { id: provider.id, name: provider.name, isMock: true };
      }
    });
    await Promise.all(fetchPromises);
    results['system_status'] = system_status;
    return results;
  }
  async fetchByProviderId(id: string): Promise<unknown | null> {
    const provider = this.providers.get(id);
    if (!provider) {
      console.error(`Provider with ID ${id} not found.`);
      return null;
    }
    try {
      return await this.fetchWithCache(provider);
    } catch (error) {
      console.error(`Error in provider ${provider.name}:`, error);
      return null;
    }
  }
  private async fetchWithCache(provider: IDataSource<unknown>): Promise<unknown | null> {
    const cacheKey = provider.getCacheKey();
    try {
      const cached = await prisma.cacheEntry.findUnique({
        where: { providerId: cacheKey },
      });
      if (cached && (Date.now() - cached.updatedAt.getTime() < CACHE_TTL_MS)) {
        console.log(`[Cache HIT] ${provider.name}`);
        return JSON.parse(cached.data);
      }
      console.log(`[Cache MISS] Fetching fresh data for ${provider.name}`);
      const data = await provider.fetchData();
      if (!provider.validateResponse(data)) {
        console.error(`Validation failed for provider: ${provider.name}`);
        return cached ? JSON.parse(cached.data) : null;
      }
      await prisma.cacheEntry.upsert({
        where: { providerId: cacheKey },
        update: { data: JSON.stringify(data) },
        create: { providerId: cacheKey, data: JSON.stringify(data) },
      });
      return data;
    } catch (error) {
      console.error(`Cache/Fetch error for ${provider.name}:`, error);
      return null;
    }
  }
  getProvidersByCategory(category: string): IDataSource<unknown>[] {
    return Array.from(this.providers.values()).filter(p => p.category === category);
  }
}
