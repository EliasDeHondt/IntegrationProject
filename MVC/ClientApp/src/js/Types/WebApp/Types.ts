export type Feed = {
    ideas: Idea[]
    title: string
    id: number
}

export type Idea = {
    id: number
    author: Author
    likes: Like[]
    text: string
    image: string | null
}

export type Author = {
    email: string
    name: string
}

export type Like = {
    liker: Author
}

export type Reaction = {
    author: Author,
    text: string
}