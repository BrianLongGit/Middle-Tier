  //writes Order Sheet JSON to a a Tab Delimited EDI text file
  using (StreamWriter OrderSheet = new StreamWriter(@"C:\Users\blong\Desktop\OrderSheet.txt"))
  {
      var recordNumber = 1;
      for (var i = 0; i < rootObject.orders.Count; i++)//rootObject.orders.Count
      {          

          OrderHeaderRecord orderHeaderRecord = new OrderHeaderRecord
          {
              record_number = recordNumber.ToString().PadLeft(6, '0'),
              record_type = "HDR",
              order_pick_number = rootObject.orders[i].order_pick_number,
              ship_via_code = rootObject.orders[i].shipping_method.PadRight(4, ' '),
              dc_number = rootObject.orders[i].dc_number,
              store_number = rootObject.orders[i].store_number,
              department = rootObject.orders[i].department,
              division = rootObject.orders[i].division,
              cust_release_contract = rootObject.orders[i].cust_release_contract,
              do_not_mix_orders = rootObject.orders[i].do_not_mix_orders,
              priority = rootObject.orders[i].priority.ToString().PadLeft(2, '0'),
              ucc_label_format = rootObject.orders[i].ucc_label_format,
              start_date = rootObject.orders[i].order_date.ToString("yyyyMMdd"),
              ship_by_date = rootObject.orders[i].must_ship_by_date.ToString("yyyyMMdd"),
              cancel_date = rootObject.orders[i].cancel_date.ToString("yyyyMMdd"),
              freight_terms_code = rootObject.orders[i].freight_terms_code,
              cust_po_number = rootObject.orders[i].customer.cust_po_number.ToString().PadRight(20, ' '),
              customer_number = rootObject.orders[i].customer.customer_number,
              customer_name = rootObject.orders[i].customer.first_name,
              bill_to_addr_1 = rootObject.orders[i].customer.street_address,
              bill_to_addr_2 = rootObject.orders[i].customer.street_address2,
              bill_to_addr_3 = rootObject.orders[i].customer.street_address3,
              bill_to_city = rootObject.orders[i].customer.city,
              bill_to_state = rootObject.orders[i].customer.state,
              bill_to_zip = rootObject.orders[i].customer.zip_postal_code,
              bill_to_country = rootObject.orders[i].customer.country.PadRight(3, ' '),
              bill_to_phone = rootObject.orders[i].customer.bill_to_phone,
              ship_to_name = rootObject.orders[i].shipping_address.first_name + ' ' + rootObject.orders[i].shipping_address.last_name,
              ship_to_addr_1 = rootObject.orders[i].shipping_address.address1,
              ship_to_addr_2 = rootObject.orders[i].shipping_address.address2,
              ship_to_addr_3 = rootObject.orders[i].shipping_address.address3,
              ship_to_city = rootObject.orders[i].shipping_address.city,
              ship_to_state = rootObject.orders[i].shipping_address.state,
              ship_to_zip = rootObject.orders[i].shipping_address.zip_postal_code,
              ship_to_country = rootObject.orders[i].shipping_address.country.PadRight(3, ' '),
              currency = rootObject.orders[i].currency,
              customer_supplier_number = rootObject.orders[i].customer_supplier_number,
              customers_division = rootObject.orders[i].customers_division,
              customer_type = rootObject.orders[i].customer_type,
              customer_dept_desc = rootObject.orders[i].customer_dept_desc,
              order_type = rootObject.orders[i].order_type,
              warehouse = rootObject.orders[i].warehouse,
              terms_code = rootObject.orders[i].terms_code,
              sales_code = rootObject.orders[i].sales_code,
              store_sort_key = rootObject.orders[i].store_sort_key,
              company_number = rootObject.orders[i].company_number,
              store_name = rootObject.orders[i].store_name,
              store_addr_1 = rootObject.orders[i].store_addr_1,
              store_addr_2 = rootObject.orders[i].store_addr_2,
              store_addr_3 = rootObject.orders[i].store_addr_3,
              store_city = rootObject.orders[i].store_city,
              store_state = rootObject.orders[i].store_state,
              store_zip = rootObject.orders[i].store_zip,
              store_country = rootObject.orders[i].store_country,
              customer_email = rootObject.orders[i].customer_email,
              ups_account_number = rootObject.orders[i].ups_account_number,
              fedex_account_number = rootObject.orders[i].fedex_account_number,
              customer_tax_id = rootObject.orders[i].customer.customer_tax_id.PadRight(3, ' '),
          };

          //Loop to Create Order Sheet OHR
          //Declare empty strings for each section of Order Sheet file
          string OHRStringVar = string.Empty;
          
          //Declares new stringbuilder for each section of Order Sheet file
          StringBuilder OHRStringBuilder = new StringBuilder();
          
          //Gets properties of orderHeaderRecord to loop over properties
          var orderHeaderRecordProps = orderHeaderRecord.GetType().GetProperties();
          
          //Loop for OrderHeaderRecord properties
          foreach (var prop in orderHeaderRecordProps)
          {
              OHRStringVar += prop.GetValue(orderHeaderRecord) + "\t";
          }
          
          //To remove tab at the end of the string?
          //OHRStringVar = OHRStringVar.Substring(0, OHRStringVar.Length - 1);
          
          //Append String Variable to StringBuilder
          OHRStringBuilder.Append(OHRStringVar);
          string OHRLine = OHRStringBuilder.ToString();
          
          //writes sections to Ordersheet using StreamWriter
          OrderSheet.WriteLine(OHRLine);

          recordNumber++;


    }

}
