export default interface EditAllowanceModalProps extends ModalProps {
    onAllowanceUpdated(): void,
    selectedAllowance: object
}