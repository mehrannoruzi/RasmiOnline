$().ready(function () {
    $.validator.addMethod("nationalCode",
          function (value, element) {
              if (value.length == 0)
                  return true;
              else if (value.length == 10) {
                  if (value == '1111111111' || value == '0000000000' || value == '2222222222' || value == '3333333333' || value == '4444444444' || value == '5555555555' || value == '7777777777' || value == '8888888888' || value == '9999999999')
                      return false;
                  else if (value.charAt(0) == '0' && value.charAt(1) == '0' && value.charAt(2) == '0' && value.charAt(3) == '0' && value.charAt(4) == '0' && value.charAt(5) == '0' && value.charAt(6) == '0')
                      return false;
                  else {
                      c = parseInt(value.charAt(9));
                      n = parseInt(value.charAt(0)) * 10 + parseInt(value.charAt(1)) * 9 + parseInt(value.charAt(2)) * 8 + parseInt(value.charAt(3)) * 7 + parseInt(value.charAt(4)) * 6 + parseInt(value.charAt(5)) * 5 + parseInt(value.charAt(6)) * 4 + parseInt(value.charAt(7)) * 3 + parseInt(value.charAt(8)) * 2;
                      r = n - parseInt(n / 11) * 11;
                      if ((r == 0 && r == c) || (r == 1 && c == 1) || (r > 1 && c == 11 - r))
                          return true;
                      else
                          return false;
                  }
              }
              else
                  return false;
          },
          "<span class='requiredFieldStyle'>شماره ملي را تصحيح كنيد</span>"
     );

    $.validator.addMethod("date",
          function (value, element) {
              var result = /^(?:1[23]\d{2})\/(?:0?[1-9]|1[0-2])\/(?:0?[1-9]|[12][0-9]|3[01])$/.test(value);
              if (value.length == 0)
                  return true;
              else
                  return result;
          },
           "<span class='requiredFieldStyle'>تاريخ را تصحيح كنيد<span dir='ltr' style='margin-right: 5px;'> 1366/08/14 نمونه </span></span>"
     );

    $.validator.addMethod("mobileNumber",
          function (value, element) {
              if ((value.charAt(0) != "0" || value.charAt(1) != "9"))
                  return false;
              else
                  return true;
          },
           "<span class='requiredFieldStyle'>شماره موبایل باید با 09 آغاز شود</span>"
     );

    $("form").validate();
});