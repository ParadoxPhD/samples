//rust 1.30.0

fn main()
{
    let mut input = [-1,0,-3,1,1,4,2];
    input.sort();
    
    search(&input);
}

fn search(input: &[i32])
{
    let length = input.len();
    
    if input[length - 1] <= 0
    {
        print!("no pos");
        return ();
    }

    for i in 0..(length - 1)
    {
        let x = input[i];
        let y = input[i+1];

        if (y - x > 1) && !(y + x <= y)
        {
            print!("{}", input[i]+1);
            return ();
        }
    }

    print!("{}", input[length - 1] + 1);
    return ();
}