class MoneyUtil {
  static formatMoney(amount:any) {
    // Chia tiền thành các phần ngăn cách bởi dấu chấm
    return amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }

  static splitMoney(amount:any) {
    // Tách số tiền thành phần nguyên và phần thập phân
    const [integerPart, decimalPart] = amount.toString().split(".");
    // Định dạng phần nguyên
    const formattedIntegerPart = this.formatMoney(integerPart);

    return decimalPart ? `${formattedIntegerPart},${decimalPart}` : formattedIntegerPart;
  }
}
export default MoneyUtil;