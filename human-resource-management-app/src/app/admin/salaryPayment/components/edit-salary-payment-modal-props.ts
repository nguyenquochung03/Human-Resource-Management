export default interface EditSalaryPaymentModalProps extends ModalProps{
    onSalaryPaymentUpdated() : void,
    selectedSalaryPayment: object
}