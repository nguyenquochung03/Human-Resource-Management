interface ConfirmModalProps {
    title: string;
    description: string;
    isOpen: boolean;
    handleCancel: () => void;
    handleConfirm: () => void;
}