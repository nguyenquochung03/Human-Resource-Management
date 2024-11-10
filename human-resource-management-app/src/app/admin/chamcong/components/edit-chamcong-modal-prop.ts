import ChamCong from "@/models/chamCong";

export default interface EditChamCongModalProps extends ModalProps {
    onChamCongUpdated(): void,
    selectedChamCong: object,
}