using Stripe;
using Automated_Wedding_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Automated_Wedding_Application.Areas.Identity.Data;
using Automated_Wedding_Application.Data;
using Microsoft.AspNetCore.Identity;
using Automated_Wedding_Application.Models;
using Automated_Wedding_Application.Services;


namespace Automated_Wedding_Application.Payment
{
    public class ProcessPayment : Controller
    {
       
        public static async Task<dynamic> PayAsync(PayModel payModel, List<PayoutModel> payoutModels,string email)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51IwcAxDO2neTlgEMvv2UNq1N4MzjY37BrZR3m0PdmKF7SADZ1slVqc4VfTOpQwxaFNM5naMX7d8lTWv8YsmAcQcR00eUsWf5YZ";

                var options = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = payModel.CardNumder,
                        ExpMonth = payModel.Month,
                        ExpYear = payModel.Year,
                        Cvc = payModel.CVC
                    },
                };

                var serviceToken = new TokenService();
                Token stripeToken = await serviceToken.CreateAsync(options);

                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = (long?)(payModel.Amount * 100),
                    Currency = "GBP",
                    Description = "Stripe Test Payment",
                    Source = stripeToken.Id,
                    ReceiptEmail= email
                };

                var chargeService = new ChargeService();
                Charge charge = await chargeService.CreateAsync(chargeOptions);

                List<PayoutModel> payoutModels1 = new List<PayoutModel>();
            
                foreach(var payoutmodel in payoutModels)
                {
                    var transferoptions = new TransferCreateOptions
                    {
                        Amount = (long?)((payoutmodel.Amount)*100),
                        Currency = "gbp",
                        Destination = payoutmodel.Stripaccountno,
                        TransferGroup = payoutmodel.Stripaccountno,
                        
                    };
                    var service = new TransferService();
                    Transfer status = await service.CreateAsync(transferoptions);

                    PayoutModel payoutmodelref = new PayoutModel();
                    payoutmodelref.plannerid = payoutmodel.plannerid;
                    payoutmodel.plannerpaymentid = status.Id;
                    payoutModels1.Add(payoutmodel);

                }

                

                if (charge.Paid)
                {

                    var result = new { chargeid = charge.Id, status = "Success" , payoutModels= payoutModels1 };
                    return result;
                }
                else
                {
                    var result = new { status = "Failed"};
                    return result;
                }
            }
            catch(Exception ex)
            {
                var result = new { status = "Exception", error= ex.Message };
                return result;
            }
        }

        public static async Task<dynamic>  refundAsync(CustomerCart cartvalues, string userstripeId)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51IwcAxDO2neTlgEMvv2UNq1N4MzjY37BrZR3m0PdmKF7SADZ1slVqc4VfTOpQwxaFNM5naMX7d8lTWv8YsmAcQcR00eUsWf5YZ";
                var options = new TransferReversalCreateOptions
                {
                    Amount = (long?)(cartvalues.ServiceCost) * 100,
                };

                var service = new TransferReversalService();
                var reversal = service.Create(cartvalues.plannerpaymentid, options);

                var options1 = new RefundCreateOptions
                {
                    Amount = (long?)(cartvalues.ServiceCost) * 100,
                    Charge = cartvalues.Chargeid
                };

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = userstripeId;

                var service1 = new RefundService();
                var refund = service1.Create(options1);


                return "Success";

                //if (charge.Paid)
                //{
                //    var result = new { chargeid = charge.Id, status = "Success" };
                //    return result;
                //}
                //else
                //{
                //    return "Failed";
                //}
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
