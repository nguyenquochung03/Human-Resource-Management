export default interface EditContractModalProps extends ModalProps {
    onContractUpdated(): void,
    selectedContract: object
}