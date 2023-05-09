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
  artists: SimpleArtistDto[];
  type?: AlbumType;
  /**
   * @format int32
   */
  releaseYear: number;
  artworks: ArtworkDto;
  tracks: TrackDto[];
  genres: GenreDto[];
};

export type AlbumType = "Single" | "EP" | "MiniAlbum" | "Album" | "Compilation";

export type ArtistDto = {
  /**
   * @format int32
   */
  id: number;
  name: string;
  releases: SimpleAlbumDto[];
  featuredIn: SimpleAlbumDto[];
  remixerIn: SimpleAlbumDto[];
  inCompilation: SimpleAlbumDto[];
};

export type ArtistRole = "Main" | "Guest" | "Remixer";

export type ArtistWithRoleDto = {
  /**
   * @format int32
   */
  id: number;
  name: string;
  role: ArtistRole;
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

export type SearchResultPaginatedCustomData = {
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
  data: SearchResult;
};

export type SimpleAlbumDto = {
  /**
   * @format int32
   */
  id: number;
  name: string;
  artists: SimpleArtistDto[];
  type?: AlbumType;
  /**
   * @format int32
   */
  releaseYear: number;
  artworks: ArtworkDto;
};

export type SimpleAlbumDtoPaginatedQuery = {
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

export type SimpleArtistDtoPaginatedQuery = {
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
  data: SimpleArtistDto[];
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
  artists: ArtistWithRoleDto[];
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
