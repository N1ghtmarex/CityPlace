namespace Domain.Abstractions
{
    public interface IHasArchiveTrack
    {
        /// <summary>
        /// Статус архивности
        /// </summary>
        public bool IsArchive { get; set; }
    }
}
