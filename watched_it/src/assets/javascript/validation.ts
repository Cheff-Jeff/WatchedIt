const reName = /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]*$/;
const reEmail = /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/;
const reTitle = /^[a-zA-Z0-9_ #--():]*$/;
const reDescription = /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-?!"()#&+—’”“]*$/;
const reId = /^[0-9]*$/;
const rePhone = /^([0|+[0-9]{1,5})?([0-9]{10})$/

export const validateName = (name: string): boolean => {
  return reName.test(name);
}
export const validateTitle = (title: string): boolean => {
  return reTitle.test(title);
}
export const validateEmail = (email: string): boolean => {
  return reEmail.test(email);
}
export const validateDescription = (description: string): boolean => {
  return reDescription.test(description);
}
export const validateId = (id: string): boolean => {
  return reId.test(id);
}
export const validatePhone = (phone: string): boolean => {
  return rePhone.test(phone)
}

//errors
export const errNameEmp = () => { return 'Name can not be empty.' }
export const errName = (e: string) => { return `${e} is not a name.` }
export const errTitleEmp = () => { return 'Title can not be empty.' }
export const errTitle = (e: string) => { return `${e} is not a proper title.` }
export const errEmailEmp = () => { return 'Email can not be empty.' }
export const errEmail = (e: string) => { return `${e} is not an email.` }
export const errPassEmp = () => { return 'Password can not be empty.'}
export const errPass = () => { return 'Password must be at least 10 characters long.'}
export const errConfirmPass = () => { return 'Confirm password must match.'}
export const errDescriptionEmp = () => { return 'Description can not be empty.' }
export const errDescription = (e: string) => { return `${e} is not a description.` }
export const errPublicherEmp = () => { return 'Publicher can not be empty.' }
export const errPublicher = (e: string) => { return `${e} is not a publicher.` }
export const errWriterIdEmp = () => { return 'Please select a writer.' }
export const errWriterId = () => { return `That is not a writer.` }
export const errArtistIdEmp = () => { return 'Please select an artist.' }
export const errArtistId = () => { return `That is not an artist.` }
export const errCoverArtistIdEmp = () => { return 'Please select a cover artist.' }
export const errCoverArtistId = () => { return `That is not a cover artist.` }
export const errPublicherIdEmp = () => { return 'Please select a publicher.' }
export const errPublicherId = () => { return `That is not a publicher.` }
export const errImageEmp = () => { return `Image can not be empty.` }
export const errImage = () => { return `This is not an image.` }
export const errPhoneEmp = () => {return 'Phone number can not be empty.'}
export const errPhone = (e: string) => {return `${e} is not a valid phone number.`}