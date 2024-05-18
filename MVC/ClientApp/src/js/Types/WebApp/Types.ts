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
}

export type Author = {
    email: string
    name: string
}

export type Like = {
    liker: Author
}
