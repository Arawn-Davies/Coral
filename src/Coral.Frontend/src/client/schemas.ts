/**
 * Generated by @openapi-codegen
 *
 * @version v1
 */
export type AlbumDto = {
  /**
   * @format int32
   */
  id: number;
  name: string;
  artists: ArtistDto[];
  tracks: TrackDto[];
  genres: GenreDto[];
  /**
   * @format int32
   */
  releaseYear: number;
};

export type ArtistDto = {
  /**
   * @format int32
   */
  id: number;
  name: string;
};

export type ArtworkDto = {
  small: string;
  medium: string;
  original: string;
};

export type GenreDto = {
  /**
   * @format int32
   */
  id: number;
  name: string;
};

export type OutputFormat = "AAC" | "MP3" | "Ogg" | "Opus";

export type SearchResult = {
  artists: SimpleArtistDto[];
  albums: SimpleAlbumDto[];
  tracks: TrackDto[];
};

export type SimpleAlbumDto = {
  /**
   * @format int32
   */
  id: number;
  name: string;
  artists: SimpleArtistDto[];
  /**
   * @format int32
   */
  releaseYear: number;
  coverPresent: boolean;
};

export type SimpleAlbumDtoListPaginatedData = {
  /**
   * @format int32
   */
  availableRecords: number;
  /**
   * @format int32
   */
  totalRecords: number;
  /**
   * @format int32
   */
  resultCount: number;
  data: SimpleAlbumDto[];
};

export type SimpleArtistDto = {
  /**
   * @format int32
   */
  id: number;
  name: string;
};

export type StreamDto = {
  link: string;
  transcodeInfo?: TranscodeInfoDto;
  artworkUrl?: string | null;
};

export type TrackDto = {
  /**
   * @format int32
   */
  id: number;
  title: string;
  /**
   * @format int32
   */
  durationInSeconds: number;
  comment?: string | null;
  /**
   * @format int32
   */
  trackNumber: number;
  /**
   * @format int32
   */
  discNumber: number;
  artist: SimpleArtistDto;
  album: SimpleAlbumDto;
  genre?: GenreDto;
};

export type TranscodeInfoDto = {
  /**
   * @format uuid
   */
  jobId: string;
  format: OutputFormat;
  /**
   * @format int32
   */
  bitrate: number;
};
