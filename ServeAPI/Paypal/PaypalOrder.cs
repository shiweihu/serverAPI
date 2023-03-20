using System;
namespace ServeAPI.Paypal
{
    public class Address
    {
        public string country_code { get; set; }
        public string address_line_1 { get; set; } 
        public string admin_area_2 { get; set; }
        public string admin_area_1 { get; set; } 
        public string postal_code { get; set; }

        public Address(string country_code, string address_line_1, string admin_area_2, string admin_area_1, string postal_code)
        {
            this.country_code = country_code;
            this.address_line_1 = address_line_1;
            this.admin_area_2 = admin_area_2;
            this.admin_area_1 = admin_area_1;
            this.postal_code = postal_code;
        }
    }

    public class Amount
    {
        public string currency_code { get; set; }
        public string value { get; set; }
        public Breakdown breakdown { get; set; }

        public Amount(string currency_code, string value, Breakdown breakdown)
        {
            this.currency_code = currency_code;
            this.value = value;
            this.breakdown = breakdown;
        }
    }

    public class Breakdown
    {
        public ItemTotal item_total { get; set; }

        public Breakdown(ItemTotal item_total)
        {
            this.item_total = item_total;
        }
    }

    public class Capture
    {
        public string id { get; set; }
        public string status { get; set; }
        public Amount amount { get; set; }
        public bool final_capture { get; set; }
        public SellerProtection seller_protection { get; set; }
        public SellerReceivableBreakdown seller_receivable_breakdown { get; set; }
        public List<Link> links { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }

        public Capture(string id, string status, Amount amount, bool final_capture, SellerProtection seller_protection, SellerReceivableBreakdown seller_receivable_breakdown, List<Link> links, DateTime create_time, DateTime update_time)
        {
            this.id = id;
            this.status = status;
            this.amount = amount;
            this.final_capture = final_capture;
            this.seller_protection = seller_protection;
            this.seller_receivable_breakdown = seller_receivable_breakdown;
            this.links = links;
            this.create_time = create_time;
            this.update_time = update_time;
        }
    }

    public class GrossAmount
    {
        public string currency_code { get; set; }
        public string value { get; set; }

        public GrossAmount(string currency_code, string value)
        {
            this.currency_code = currency_code;
            this.value = value;
        }
    }

    public class Item
    {
        public string name { get; set; }
        public UnitAmount unit_amount { get; set; }
        public string quantity { get; set; }

        public Item(string name, UnitAmount unit_amount, string quantity)
        {
            this.name = name;
            this.unit_amount = unit_amount;
            this.quantity = quantity;
        }
    }

    public class ItemTotal
    {
        public string currency_code { get; set; }
        public string value { get; set; }

        public ItemTotal(string currency_code, string value)
        {
            this.currency_code = currency_code;
            this.value = value;
        }
    }

    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }

        public Link(string href, string rel, string method)
        {
            this.href = href;
            this.rel = rel;
            this.method = method;
        }
    }

    public class Name
    {
        public string given_name { get; set; }
        public string surname { get; set; }
        public string full_name { get; set; }

        public Name(string given_name, string surname, string full_name)
        {
            this.given_name = given_name;
            this.surname = surname;
            this.full_name = full_name;
        }
    }

    public class NetAmount
    {
        public string currency_code { get; set; }
        public string value { get; set; }

        public NetAmount(string currency_code, string value)
        {
            this.currency_code = currency_code;
            this.value = value;
        }
    }

    public class Payee
    {
        public string email_address { get; set; }
        public string merchant_id { get; set; }

        public Payee(string email_address, string merchant_id)
        {
            this.email_address = email_address;
            this.merchant_id = merchant_id;
        }
    }

    public class Payer
    {
        public Name name { get; set; }
        public string email_address { get; set; }
        public string payer_id { get; set; }
        public Address address { get; set; }

        public Payer(Name name, string email_address, string payer_id, Address address)
        {
            this.name = name;
            this.email_address = email_address;
            this.payer_id = payer_id;
            this.address = address;
        }
    }

    public class Payments
    {
        public List<Capture> captures { get; set; }

        public Payments(List<Capture> captures)
        {
            this.captures = captures;
        }
    }

    public class PaymentSource
    {
        public Paypal paypal { get; set; }

        public PaymentSource(Paypal paypal)
        {
            this.paypal = paypal;
        }
    }

    public class Paypal
    {
        public string email_address { get; set; }
        public string account_id { get; set; }
        public Name name { get; set; }
        public Address address { get; set; }

        public Paypal(string email_address, string account_id, Name name, Address address)
        {
            this.email_address = email_address;
            this.account_id = account_id;
            this.name = name;
            this.address = address;
        }
    }

    public class PaypalFee
    {
        public string currency_code { get; set; }
        public string value { get; set; }

        public PaypalFee(string currency_code, string value)
        {
            this.currency_code = currency_code;
            this.value = value;
        }
    }

    public class PurchaseUnit
    {
        public string reference_id { get; set; }
        public Amount amount { get; set; }
        public Payee payee { get; set; }
        public string soft_descriptor { get; set; }
        public List<Item> items { get; set; }
        public Shipping shipping { get; set; }
        public Payments payments { get; set; }

        public PurchaseUnit(string reference_id, Amount amount, Payee payee, string soft_descriptor, List<Item> items, Shipping shipping, Payments payments)
        {
            this.reference_id = reference_id;
            this.amount = amount;
            this.payee = payee;
            this.soft_descriptor = soft_descriptor;
            this.items = items;
            this.shipping = shipping;
            this.payments = payments;
        }
    }

    public class PaypalOrder
    {
        public string id { get; set; }
        public string intent { get; set; }
        public string status { get; set; }
        public PaymentSource payment_source { get; set; }
        public List<PurchaseUnit> purchase_units { get; set; }
        public Payer payer { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public List<Link> links { get; set; }

        public PaypalOrder(string id, string intent, string status, PaymentSource payment_source, List<PurchaseUnit> purchase_units, Payer payer, DateTime create_time, DateTime update_time, List<Link> links)
        {
            this.id = id;
            this.intent = intent;
            this.status = status;
            this.payment_source = payment_source;
            this.purchase_units = purchase_units;
            this.payer = payer;
            this.create_time = create_time;
            this.update_time = update_time;
            this.links = links;
        }
    }

    public class SellerProtection
    {
        public string status { get; set; }
        public List<string> dispute_categories { get; set; }

        public SellerProtection(string status, List<string> dispute_categories)
        {
            this.status = status;
            this.dispute_categories = dispute_categories;
        }
    }

    public class SellerReceivableBreakdown
    {
        public GrossAmount gross_amount { get; set; }
        public PaypalFee paypal_fee { get; set; }
        public NetAmount net_amount { get; set; }

        public SellerReceivableBreakdown(GrossAmount gross_amount, PaypalFee paypal_fee, NetAmount net_amount)
        {
            this.gross_amount = gross_amount;
            this.paypal_fee = paypal_fee;
            this.net_amount = net_amount;
        }
    }

    public class Shipping
    {
        public Name name { get; set; }
        public Address address { get; set; }

        public Shipping(Name name, Address address)
        {
            this.name = name;
            this.address = address;
        }
    }

    public class UnitAmount
    {
        public string currency_code { get; set; }
        public string value { get; set; }

        public UnitAmount(string currency_code, string value)
        {
            this.currency_code = currency_code;
            this.value = value;
        }
    }
}

