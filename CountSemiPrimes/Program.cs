// you can also use imports, for example:
// import java.util.*;

// you can write to stdout for debugging purposes, e.g.
// System.out.println("this is a debug message");

class Solution
{
    public int[] solution(int N, int[] P, int[] Q)
    {
        // write your code in Java SE 8

        //Do Prime Sieve Eratosthenes, modified to update an array with the smallest prime divisor of the item
        int[] primeFactorSieve = GetFactorizationSieve(N);

        //For each number up to N, check if val = prime-sieve-min-divisor array > 0, && prime-sieve-min-divisor array of n / val is 0.
        // If so it's a semiprime(product of 2 primes), so add to prefix sum array of cumulative up to now.
        int cumulativePrimes = 0;
        int[] prefixSumPrimeCount = new int[N+1];
        for(int i = 2; i <= N; i++) //Ignore 1 and 2 as they are never going to be
        {
            if(primeFactorSieve[i] > 0 && primeFactorSieve[i / primeFactorSieve[i]] == 0)
            {
                //Then semiprime so update array of counts
                cumulativePrimes++;
            }
            prefixSumPrimeCount[i] = cumulativePrimes;
        }

        //Now go through sections of the array defined in P and Q and get the number of semiprimes in here
        int[] result = new int[P.Length];
        for(int i = 0; i < P.Length; i++)
        {
            int rangeStart = P[i];
            int rangeEnd = Q[i];
            result[i] = prefixSumPrimeCount[rangeEnd] - prefixSumPrimeCount[rangeStart - 1];

        }
        return result;
    }

    public bool[] GetPrimeSieve(int n)
    {
        bool[] sieve = new bool[n+1];
        sieve[0] = sieve[1] = false; // 0 and 1 will never have factors
        int i = 2;
        while(i * i <= n) // Once it's larger then n is not going to be a symmetric factor, so no point carrying on.
        {
            if(sieve[i]) //If the value has not been removed already
            {
                int k = i * i; //Mathematical proof req. We do not need to look at those vals lower than i*i as they will be covered by other smaller vals already
                while(k <= n)
                {
                    sieve[k] = false; // i is a factor of k, so remove.
                    k += i;
                }
            }
            i++;
        }

        return sieve;
    }

    public int[] GetFactorizationSieve(int n)
    {
        int[] sieve = new int[n+1];
        int i = 2;
        while(i * i <= n)
        {
            if(sieve[i] == 0) // If we are a prime ourselves
            {
                int k = i * i;
                while(k <= n)
                {
                    if (sieve[k] == 0) //If element has not had a smaller prime added already.
                    {
                        sieve[k] = i;
                    }
                    k += i;
                }
            }
            i++;
        }
        return sieve;
    }
}