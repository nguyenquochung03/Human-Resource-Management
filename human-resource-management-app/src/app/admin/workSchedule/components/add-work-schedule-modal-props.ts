export default interface AddWorkScheduleModal extends ModalProps {
    isOpen: boolean,
    handleCancel(): void,
    onWorkScheduleCreated(): void
}