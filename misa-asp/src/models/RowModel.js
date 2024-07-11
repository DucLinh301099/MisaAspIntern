

class RowModel {
  constructor(
    description = "",
    debitAccount = null,
    creditAccount = null,
    amount = "",
    objectId = null,
    objectName = ""
  ) {
    this.description = description;
    this.debitAccount = debitAccount;
    this.creditAccount = creditAccount;
    this.amount = amount;
    this.objectId = objectId;
    this.objectName = objectName;
  }
}

export default RowModel;
