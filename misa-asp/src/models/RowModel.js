

class RowModel {
  constructor(
    description = "",
    debitAccountNumber = "",
    creditAccountNumber = "",
    amount = "",
    objectId = null,
    objectName = ""
  ) {
    this.description = description;
    this.debitAccountNumber = debitAccountNumber;
    this.creditAccountNumber = creditAccountNumber;
    this.amount = amount;
    this.objectId = objectId;
    this.objectName = objectName;
  }
}

export default RowModel;
