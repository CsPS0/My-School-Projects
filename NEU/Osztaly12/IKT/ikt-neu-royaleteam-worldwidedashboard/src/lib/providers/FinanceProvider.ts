import { BaseDataSource } from '../core/DataSource';
import axios from 'axios';
export interface FinanceData {
  crypto: {
    bitcoin: { usd: number; eur: number };
    ethereum: { usd: number; eur: number };
    solana: { usd: number; eur: number };
    binancecoin: { usd: number; eur: number };
    dogecoin: { usd: number; eur: number };
    hawkTuah: { usd: number; eur: number };
  };
  currency: {
    EUR_TO_HUF: number;
    EUR_TO_USD: number;
  };
}
export class FinanceProvider extends BaseDataSource<FinanceData> {
  id = 'finance_markets';
  name = 'Global Finance Markets';
  category = 'Finance';
  isMock = true;
  async fetchData(): Promise<FinanceData> {
    try {
      const cryptoRes = await axios.get(
        'https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum,solana,binancecoin,dogecoin,hawk-tuah&vs_currencies=usd,eur'
      );
      const currencyRes = await axios.get(
        'https://api.frankfurter.app/latest?from=EUR&to=HUF,USD'
      );
      const cryptoData = cryptoRes.data || {};
      return {
        crypto: {
          bitcoin: {
            usd: cryptoData.bitcoin?.usd || 0,
            eur: cryptoData.bitcoin?.eur || 0,
          },
          ethereum: {
            usd: cryptoData.ethereum?.usd || 0,
            eur: cryptoData.ethereum?.eur || 0,
          },
          solana: {
            usd: cryptoData.solana?.usd || 0,
            eur: cryptoData.solana?.eur || 0,
          },
          binancecoin: {
            usd: cryptoData.binancecoin?.usd || 0,
            eur: cryptoData.binancecoin?.eur || 0,
          },
          dogecoin: {
            usd: cryptoData.dogecoin?.usd || 0,
            eur: cryptoData.dogecoin?.eur || 0,
          },
          hawkTuah: {
            usd: cryptoData['hawk-tuah']?.usd || 0,
            eur: cryptoData['hawk-tuah']?.eur || 0,
          },
        },
        currency: {
          EUR_TO_HUF: currencyRes.data.rates?.HUF || 395.5,
          EUR_TO_USD: currencyRes.data.rates?.USD || 1.08,
        },
      };
    } catch (error) {
      return this.handleFetchError(error);
    }
  }
  validateResponse(data: unknown): boolean {
    if (typeof data !== 'object' || data === null) return false;
    const d = data as Record<string, unknown>;
    return (
      'crypto' in d &&
      'currency' in d &&
      typeof d.crypto === 'object' && d.crypto !== null &&
      typeof d.currency === 'object' && d.currency !== null
    );
  }
}
