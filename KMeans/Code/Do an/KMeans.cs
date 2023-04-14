using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Do_an
{
    public class KMeans
    {
        public static List<List<Customer>> Cluster(List<Customer> customers, int k)
        {
            List<List<Customer>> clusters = new List<List<Customer>>();

            // Initialize cluster centers randomly
            List<Customer> centers = new List<Customer>();
            for (int i = 0; i < k; i++)
            {
                centers.Add(customers[rand.Next(customers.Count)]);
            }

            // Assign each customer to its nearest cluster center
            foreach (var customer in customers)
            {
                double minDistance = double.MaxValue;
                int minIndex = -1;

                for (int i = 0; i < centers.Count; i++)
                {
                    double distance = EuclideanDistance(customer, centers[i]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minIndex = i;
                    }
                }

                if (clusters.Count <= minIndex)
                {
                    clusters.Add(new List<Customer>());
                }

                clusters[minIndex].Add(customer);
            }

            // Re-compute cluster centers
            bool centersChanged = true;
            while (centersChanged)
            {
                centersChanged = false;

                for (int i = 0; i < clusters.Count; i++)
                {
                    double sumAge = 0;
                    double sumIncome = 0;
                    foreach (var customer in clusters[i])
                    {
                        sumAge += customer.age;
                        sumIncome += customer.income;
                    }

                    double newAge = sumAge / clusters[i].Count;
                    double newIncome = sumIncome / clusters[i].Count;

                    Customer newCenter = new Customer(-1, "", newAge, newIncome);
                    double distanceToOldCenter = EuclideanDistance(newCenter, centers[i]);

                    if (distanceToOldCenter > 0.001)
                    {
                        centersChanged = true;
                        centers[i] = newCenter;

                        // Re-assign customers to clusters
                        clusters = new List<List<Customer>>();
                        for (int j = 0; j < k; j++)
                        {
                            clusters.Add(new List<Customer>());
                        }

                        foreach (var customer in customers)
                        {
                            double minDistance = double.MaxValue;
                            int minIndex = -1;

                            for (int j = 0; j < centers.Count; j++)
                            {
                                double distance = EuclideanDistance(customer, centers[j]);
                                if (distance < minDistance)
                                {
                                    minDistance = distance;
                                    minIndex = j;
                                }
                            }

                            clusters[minIndex].Add(customer);
                        }

                        break;
                    }
                }
            }

            return clusters;
        }

        private static double EuclideanDistance(Customer customer1, Customer customer2)
        {
            double diffAge = customer1.age - customer2.age;
            double diffIncome = customer1.income - customer2.income;
            double distance = Math.Sqrt(diffAge * diffAge + diffIncome * diffIncome);
            return distance;
        }
    }
}
